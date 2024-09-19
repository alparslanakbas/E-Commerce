import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxCustomModalComponent } from 'ngx-custom-modal';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent{

  constructor() { }

  params = {
    id: null,
    title: '',
};
paramsTask = {
    projectId: null,
    id: null,
    title: '',
    description: '',
};
selectedTask: any = null;
projectList: any = [
    {
        id: 2,
        title: 'Yapılacaklar',
        tasks: [
            {
                projectId: 2,
                id: 2.1,
                title: 'Ekleme Modal Fix',
                description: 'Ekleme işlemi yaparken özelliklerin hata mesajları apiden getirildi fakat alert üzerinde henüz gösterimi yapılmadı.',
                date: '19.09.2024 Perşembe',
            },
        ],
    },
    {
        id: 3,
        title: 'Bitenler',
    }
];
}
