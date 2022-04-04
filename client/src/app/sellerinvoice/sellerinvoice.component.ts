import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../orders/orders.service';
import { Isellerinvoice } from '../shared/models/invoice';


@Component({
  selector: 'app-sellerinvoice',
  templateUrl: './sellerinvoice.component.html',
  styleUrls: ['./sellerinvoice.component.scss']
})
export class SellerinvoiceComponent implements OnInit {
  invoice: Isellerinvoice[];

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.generateinvoice();
  }
  generateinvoice() {
    this.generatesellerinvoice().subscribe((invoices: Isellerinvoice[]) => {
      this.invoice = invoices;
    }, error => {
      console.log(error);
    })
  }
  generatesellerinvoice() {
    return this.http.get('https://localhost:5001/api/Basket/invoice?name='+localStorage.getItem('email'));
  }


}