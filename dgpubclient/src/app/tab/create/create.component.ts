import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TabService } from '../services/tab.service';
import { Router } from '@angular/router';
import { Tab } from '../models/tab';
import { CreateTab } from '../models/createTab';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  createForm: FormGroup;  
  public msgs: any[] = [];

  constructor(private fb: FormBuilder,
    private tabService: TabService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.createForm = this.fb.group({
      customerName: ['', [Validators.required]]      
    });
  }

  create(){
    if (this.createForm.dirty && this.createForm.valid) {     
      let tab = new CreateTab();
      tab.customerName = this.createForm.value.customerName;
      this.tabService.create(tab)
        .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }    
  }

  onSaveComplete(response: Tab): void { 
    this.router.navigate(['/manager', response.id]);
  }

  onError(fail: any) {    
    this.msgs = fail.error.errors;
  }

}
