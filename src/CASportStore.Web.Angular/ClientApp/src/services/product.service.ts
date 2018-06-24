import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get('/api/product').map(
      res => res
    );
  }

  saveProduct(product: any) {
    return this.http.post('/api/product', product).map(
      res => res
    );
  }

}
