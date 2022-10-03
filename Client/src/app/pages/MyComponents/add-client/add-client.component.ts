import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BasicService } from 'src/app/Services/basic.service';

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.scss']
})
export class AddClientComponent implements OnInit {
  clientForm = new FormGroup({
    Id: new FormControl(''),
    ClientName: new FormControl(''),
    Address: new FormControl(''),
    Responsible: new FormControl('')
  });
  constructor(private service:BasicService) { }

  ngOnInit(): void {
  }
  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.clientForm.value);
    this.service.insertClient(this.clientForm.value)
  }
}
