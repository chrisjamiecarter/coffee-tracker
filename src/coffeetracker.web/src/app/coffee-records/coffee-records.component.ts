import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { CoffeeRecord } from '../shared/coffee-record.interface';
import { CoffeeRecordService } from '../shared/coffee-record.service';

@Component({
  selector: 'app-coffee-records',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './coffee-records.component.html',
  styleUrl: './coffee-records.component.css'
})
export class CoffeeRecordsComponent {
  coffeeRecordService: CoffeeRecordService = inject(CoffeeRecordService);

  coffeeRecords: CoffeeRecord[] = [];

  constructor() {
    this.coffeeRecordService
    .getAllCoffeeRecords()
    .then((coffeeRecords: CoffeeRecord[]) => {
      this.coffeeRecords = coffeeRecords;
    })
  }
}
