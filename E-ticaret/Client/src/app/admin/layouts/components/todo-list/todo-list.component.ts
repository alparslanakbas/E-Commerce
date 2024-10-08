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
                title: 'Ekleme toastr Fix',
                description: 'Ekleme işlemi yaparken özelliklerin hata mesajları apiden getirildi fakat toastr üzerinde henüz gösterimi yapılmadı.',
                date: '19.09.2024 Perşembe',
            }, 
            {
                projectId: 5,
                id: 2.4,
                title: 'CRUD Modal fix',
                description: 'Ekleme, silme ve güncelleme modalların dan dönen mesajlar için multi dil entegresi yapılmalı.',
                date: '21.09.2024 Cumartesi',
            },
            {
                projectId: 7,
                id: 2.6,
                title: 'Datatable Dataları indirme',
                description: 'Tabloda ki seçilen kayıt kadar indirme yapmayıp bütün dataları indirmeye çalışıyor. Csv,Txt ve print uzantısı düzenlenecek.',
                date: '21.09.2024 Cumartesi',
            },
            
            {
                projectId: 9,
                id: 2.8,
                title: 'Sweet alert çeviri',
                description: 'Sweet alert bir servise entegre edilip multi dil çevirisine dönüştürülmelidir.',
                date: '23.09.2024 Pazartesi',
            },

            {
                projectId: 11,
                id: 3.0,
                title: 'Tablodaki filtrelemer',
                description: 'tablo daki dinamik gelen filtreleme araçları çalışmıyor düzeltilmesi gerek',
                date: '06.10.2024 Pazar',
            },
        ],
    },
    {
        id: 3,
        title: 'Bitenler',
        tasks:[
            {
                projectId: 3,
                id: 2.2,
                title: 'yükleniyor butonu',
                description: 'Listeleme sırasında yükleniyor butonu getirilmeli. 1 milyon veri olduğu için sayfa 12 saniyede render ediliyor.',
                date: '21.09.2024 Cumartesi',
            },
            {
                projectId: 4,
                id: 2.3,
                title: 'Datatable eksik translate',
                description: 'Tabloda pagination info için multi dil entegresi yapılmalı.',
                date: '21.09.2024 Cumartesi',
            },
            {
                projectId: 6,
                id: 2.5,
                title: 'Datatable Fix',
                description: 'Tabloda ki kayıt sayısı seçildiğin de bilgi mesajı sabit kalıyor',
                date: '21.09.2024 Cumartesi',
            },
            {
                projectId: 8,
                id: 2.7,
                title: 'Ekleme işlemi',
                description: 'Ekleme işleminde problem var çözülmesi gerek.',
                date: '23.09.2024 Pazartesi',
            },

            {
                projectId: 10,
                id: 2.9,
                title: 'Listeleme sırasında sayfalama hatası',
                description: 'tablo daki ürün sayıları istenilene göre geliyor fakat sayfalar gelmiyor çözülmesi gerekiyor.',
                date: '05.10.2024 Cumartesi',
            }

        ]
    }
];
}
