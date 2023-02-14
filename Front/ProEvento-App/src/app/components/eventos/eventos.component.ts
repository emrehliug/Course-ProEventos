import { Component, OnInit, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '../../models/Evento';
import { EventoService } from '../../service/evento.service';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  modalRef?: BsModalRef;
  public eventos : Evento[] = [];
  public eventosFiltrados : Evento[] = [];

  public widthImg = 100;
  public marginImg = 1;
  public exibirImagem = false;
  private filtroListado = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService ) {

  }

  public get filtroListaGet(): string{
    return this.filtroListado;
  }

  public set filtroListaSet(value: string){
    this.filtroListado = value;
    this.eventosFiltrados = this.filtroListaGet ? this.filtrarEventos(this.filtroListaGet) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public alterarImagem(){
    this.exibirImagem = !this.exibirImagem;
  }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();

    setTimeout(() => {
      /** spinner ends after 5 seconds */

    }, 5000);
  }

  public getEventos(): void {
    const observer = {
      next: (responseEvento: Evento[]) => {
        this.eventos = responseEvento;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastrService.error('Erro ao Carregar os Eventos','Erro')
      },
      complete: () => {this.spinner.hide();}
    }
    this.eventoService.getEventos().subscribe(
      observer
    );
  }


  //Modal para confirmedWindow de exclução de Eventos
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastrService.success('Evento deletado do sistema.', 'Sucesso!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  //NgxSpinner
}
