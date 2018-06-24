import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ProductService } from '../../services/product.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {  
  productForm: any;
  products: any;

  constructor(private fb: FormBuilder, private productService: ProductService) { 
    this.createForm();
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(products => {
      this.products = products.result;
      console.log(this.products);
    })
  }

  createForm(): any {
    this.productForm = this.fb.group({
      name: '',
      description: '',
      price: 0,
      category: ''
    });
  }

  add() {
    this.productService.saveProduct(this.productForm.value).subscribe(
      x => console.log(x)
    )    
  }
}
