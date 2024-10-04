import { Injectable } from '@angular/core';
import { CoffeeRecord } from './coffee-record.interface';

@Injectable({
  providedIn: 'root'
})
export class CoffeeRecordService {
  url = 'https://localhost:7010/api/v1/coffees';

  constructor() { }

  async getAllCoffeeRecords(): Promise<CoffeeRecord[]> {
    const data = await fetch(this.url);
    return (await data.json()) ?? [];
  }
}
