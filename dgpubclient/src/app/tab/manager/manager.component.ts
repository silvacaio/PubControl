import { Component, OnInit } from '@angular/core';
import { Item } from '../models/item'
import { TabService } from '../services/tab.service'
import { ItemService } from '../services/item.service'
import { Router, ActivatedRoute } from '@angular/router';
import { ItemTab } from '../models/itemTab';
import { EditTab } from '../models/editTab';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit {

  public tabId: String;
  public customerName: String;
  public total: Number;
  public msgs: any[] = [];
  public alerts: any[] = [];

  public items: Item[];

  constructor(
    private tabService: TabService,
    private itemService: ItemService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {

    // this.route.params.subscribe(
    //   params => {
    //     this.tabId = params['id'];
    //     this.loadTab(this.tabId);
    //   }
    // );

    this.itemService.getAll()
      .subscribe(items => this.items = items,
        error => this.msgs);
  }

  loadTab(id: String) {
    this.tabService.get(id)
      .subscribe(
        tab => {
          this.customerName = tab.customerName,
            this.total = tab.total
        }
      );
  }

  addItem(item: Item) {
    let itemTab = new ItemTab();
    itemTab.ItemId = item.id;
    itemTab.tabId = this.tabId;

    this.tabService.addItem(itemTab)
      .subscribe(
        tab => {
          this.customerName = tab.customerName,
            this.total = tab.total,
            this.alerts = tab.alerts
        }
      );
  }

  resetTab() {
    let editTab = new EditTab();
    editTab.id = this.tabId;

    this.tabService.reset(editTab)
      .subscribe(
        tab => {
          this.customerName = tab.customerName,
            this.total = tab.total,
            this.alerts = tab.alerts
        }
      );
  }

  closeTab() {
    this.router.navigate(['/close', this.tabId]);
  }

}
