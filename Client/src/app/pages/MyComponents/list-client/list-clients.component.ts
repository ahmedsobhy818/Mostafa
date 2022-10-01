import { Component, OnInit } from '@angular/core';
import { IClient } from 'src/app/Interfaces/client';
import { BasicService } from 'src/app/Services/basic.service';

@Component({
  selector: 'app-list-clients',
  templateUrl: './list-clients.component.html',
  styleUrls: ['./list-clients.component.scss']
})
export class ListClientsComponent implements OnInit {

  data:Array<IClient>
  constructor(private service:BasicService) {
 
   }

  ngOnInit(): void {
    this.service.getAllClients().subscribe(data=>{
       this.data=data as Array<IClient>;
    },e=>{

    })
  }

}
