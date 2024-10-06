import { Injectable } from '@angular/core';
import { CoffeeRecord } from './coffee-record.interface';
import { CreateCoffeeRecord } from './create-coffee-record.interface';

@Injectable({
  providedIn: 'root'
})
export class CoffeeRecordService {
  url = 'https://localhost:7010/api/v1/coffees';

  constructor() { }

  async addCoffeeRecord(request: CreateCoffeeRecord): Promise<CoffeeRecord[]> {
    const response = await fetch(this.url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request)
    });

    console.log("response", response)
    return (await response.json()) ?? [];
  }

  async getAllCoffeeRecords(): Promise<CoffeeRecord[]> {
    const response = await fetch(this.url);
    return (await response.json()) ?? [];
  }
}
