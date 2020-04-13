export class Car {
  selectedModel: string;
  selectedBody: string;
  engineSize: number;
  horsePower: number;
  wheelBase: number;
  peakRpm: number;
  highwayMpg: number;
  price: string;

  /**
   *
   */
  constructor(
    selectedModel: string,
    selectedBody: string,
    engineSize: number,
    horsePower: number,
    wheelBase: number,
    peakRpm: number,
    highwayMpg: number,
    price: string
  ) {
    this.engineSize = engineSize;
    this.selectedBody = selectedBody;
    this.horsePower = horsePower;
    this.selectedModel = selectedModel;
    this.wheelBase = wheelBase;
    this.peakRpm = peakRpm;
    this.highwayMpg = highwayMpg;
    this.price = price;
  }
}
