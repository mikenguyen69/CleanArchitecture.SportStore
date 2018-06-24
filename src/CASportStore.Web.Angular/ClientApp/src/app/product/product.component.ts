import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
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
      this.products = products;
      console.log(this.products);
    });
    this.productForm = new FormGroup({
      name: new FormControl(this.productForm.name, [Validators.required]),
      category: new FormControl(this.productForm.category, [Validators.required]),
      price: new FormControl(this.productForm.name, [Validators.required, Validators.min(1)]),
    });
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
