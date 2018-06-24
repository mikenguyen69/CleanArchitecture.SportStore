import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {

  constructor(private http: Http) { }

  getProducts() {
    return this.http.get('/api/product').map(
      res => res.json()
    );
  }

  saveProduct(product: any) {
    return this.http.post('/api/product', product).map(
      res => res.json()
    );
  }

}
