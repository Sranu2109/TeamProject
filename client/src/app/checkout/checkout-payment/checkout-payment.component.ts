import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit{
  @Input() checkoutForm: FormGroup;


  constructor(private http:HttpClient, private basketService: BasketService, private checkoutService: CheckoutService,
    private toastr: ToastrService, private router: Router) { }

  ngOnInit(){

  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    for(var i=0;i<basket.items.length;i++)

    {

          var id1 = basket.items[i].id;

          var q1 =basket.items[i].quantity;

          var image = basket.items[i].pictureUrl;

          var proname = basket.items[i].productName;

          var price = q1*basket.items[i].price;

          const formData = new FormData();

          formData.append("hhh",id1.toString());

          formData.append("quantity",q1.toString());

          formData.append("totalprice", price.toString());

          formData.append("imageurl",image);

          formData.append("proname",proname);




    this.http.post('https://localhost:5001/api/Basket/Ranu',formData, {reportProgress: true, observe: 'events'}).subscribe();

    }
    this.checkoutService.createOrder(orderToCreate).subscribe((order: IOrder) => {
      this.toastr.success('Order Created Successfully');
      this.basketService.deleteLocalBasket(basket.id);
      const navigationExtras: NavigationExtras = {state: order};
      this.router.navigate(['checkout/success'], navigationExtras);
    }, error => {
      this.toastr.error(error.message);
      console.log(error);
    })
  }

  private getOrderToCreate(basket: IBasket) {
    return{
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    }
  }

}