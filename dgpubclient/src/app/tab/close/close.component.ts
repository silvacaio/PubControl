import { Component, OnInit } from '@angular/core';
import { TabService } from '../services/tab.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EditTab } from '../models/editTab';
import { ItemClosedTab } from "../models/itemClosedTab";

@Component({
  selector: 'app-close',
  templateUrl: './close.component.html',
  styleUrls: ['./close.component.css']
})
export class CloseComponent implements OnInit {

  public tabId: String;
  public customerName: String;
  public total: Number;
  public totalDiscount: Number;
  public msgs: any[] = [];  

  public items: ItemClosedTab[];

  constructor(
    private tabService: TabService,    
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {

      this.route.params.subscribe(
       params => {
         this.tabId = params['id'];
         this.closeTab(this.tabId);
       }
    );
  }

  closeTab(id: String) {
    let editTab = new EditTab();
    editTab.id = id;

    this.tabService.close(editTab)
      .subscribe(
        tab => {
          this.customerName = tab.customerName,
          this.total = tab.total
          this.totalDiscount = tab.totalDiscount,
          this.items = tab.items
        }
      );
  }

  new(){
    this.router.navigate(['/create']);
  }

}
