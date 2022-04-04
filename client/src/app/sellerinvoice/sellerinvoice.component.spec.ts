import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerinvoiceComponent } from './sellerinvoice.component';

describe('SellerinvoiceComponent', () => {
  let component: SellerinvoiceComponent;
  let fixture: ComponentFixture<SellerinvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerinvoiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerinvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
