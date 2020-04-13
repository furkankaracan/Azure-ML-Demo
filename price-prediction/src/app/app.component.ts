import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ComputeService } from 'src/services/compute.service';
import { Car } from 'src/models/car';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  @ViewChild('predictForm', { static: true }) predictForm: NgForm;
  columNames: string[] = [];
  car: any = {};

  carModels: string[] = [];
  bodyStyles: string[] = [];
  predictions: Car[] = [];
  constructor(private computeService: ComputeService) {}

  ngOnInit() {
    this.carModels = this.computeService.getCarModels();
    this.bodyStyles = this.computeService.getBodyStyles();
    this.columNames = this.computeService.getColumNames();

    this.computeService.predictionAdded
    .subscribe(
      (cars: Car[]) => {
        this.predictions = cars;
      }
    );
    this.predictions = this.computeService.getPredictions();
  }

  onPredict() {
    this.computeService.predict(this.car)
    .subscribe(
      (car: Car) => {
      console.log(car);
      // tslint:disable-next-line: no-debugger
      this.computeService.addNewPrediction(car);
     },
     error => {
      alert(error);
     });
  }

  onReset(predict: NgForm) {
    predict.resetForm();
  }
}
