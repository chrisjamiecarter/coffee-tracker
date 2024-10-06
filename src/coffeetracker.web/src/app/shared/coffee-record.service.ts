import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CoffeeRecord } from './coffee-record.interface';
import { CreateCoffeeRecord } from './create-coffee-record.interface';

@Injectable({
  providedIn: 'root',
})
export class CoffeeRecordService {
  private url = 'https://localhost:7010/api/v1/coffees';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  private _coffeeRecords = new BehaviorSubject<CoffeeRecord[]>([]);
  
  public CoffeeRecords = this._coffeeRecords.asObservable();

  constructor(private http: HttpClient) {}

  addCoffeeRecord(request: CreateCoffeeRecord): void {
    this.http.post<CoffeeRecord>(this.url, request, this.httpOptions).subscribe(
      (record) => {
        this.getCoffeeRecords();
      },
      (error) => {
        console.error('ERROR - Adding Coffee Record: ', error);
      }
    );
  }

  getCoffeeRecords(): void {
    this.http.get<CoffeeRecord[]>(this.url).subscribe(
      (records) => {
        this._coffeeRecords.next(records);
      },
      (error) => {
        console.error('ERROR - Fetching Coffee Records: ', error);
      }
    );
  }
}
