import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Car } from 'src/models/car';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ComputeService {
  baseUrl = environment.apiUrl;
  predictionAdded = new Subject<Car[]>();

  private predictions: Car[] = [];

  private readonly carModels = [
    'audi',
    'alfa-remoe',
    'bmw',
    'chevrolet',
    'dodge',
    'honda',
    'isuzu',
    'jaguar',
    'mercedes-benz',
    'mitsubishi',
    'nissan',
  ];

  private readonly bodyStyles = [
    'sedan',
    'wagon',
    'hardtop',
    'dodge',
    'hatchback',
  ];

  private readonly columNames: string[] = [
    'Model',
    'Body Style',
    'Wheel Base',
    'Engine Size',
    'Horse Power',
    'Peak Rpm',
    'Highway Mpg',
    'Price',
  ];

  constructor(private http: HttpClient) {}

  getCarModels() {
    return this.carModels;
  }

  getBodyStyles() {
    return this.bodyStyles;
  }
  predict(car: Car) {
    return this.http.post(this.baseUrl + 'predict', car);
  }

  getColumNames() {
    return this.columNames;
  }

  addNewPrediction(car: Car) {
    this.predictions.push(car);
    this.predictionAdded.next(this.predictions.slice());
  }

  getPredictions() {
    return this.predictions.slice();
  }
}
