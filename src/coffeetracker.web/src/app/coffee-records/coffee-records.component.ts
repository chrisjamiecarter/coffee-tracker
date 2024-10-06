import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecord } from '../shared/coffee-record.interface';
import { CoffeeRecordService } from '../shared/coffee-record.service';
import { AddCoffeeRecordFormComponent } from './add-coffee-record-form/add-coffee-record-form.component';

@Component({
  selector: 'app-coffee-records',
  standalone: true,
  imports: [AddCoffeeRecordFormComponent, CommonModule, ReactiveFormsModule],
  templateUrl: './coffee-records.component.html',
  styleUrl: './coffee-records.component.css',
})
export class CoffeeRecordsComponent implements OnInit {
  coffeeRecords: CoffeeRecord[] = [];
  filteredCoffeeRecords: CoffeeRecord[] = [];

  filterCoffeeRecordsForm = new FormGroup({
    dateFrom: new FormControl(''),
    dateTo: new FormControl(''),
  });

  constructor(private coffeeRecordService: CoffeeRecordService) {}

  ngOnInit(): void {
    this.coffeeRecordService.CoffeeRecords.subscribe((records) => {
      this.coffeeRecords = records;
      this.filteredCoffeeRecords = records;
    });

    this.coffeeRecordService.getCoffeeRecords();
  }

  onFilter() {
    const filterDateFrom = this.filterCoffeeRecordsForm.value.dateFrom
      ? new Date(this.filterCoffeeRecordsForm.value.dateFrom)
      : null;

    const filterDateTo = this.filterCoffeeRecordsForm.value.dateTo
      ? new Date(this.filterCoffeeRecordsForm.value.dateTo)
      : null;

    console.log('Filter Records', filterDateFrom, filterDateTo);

    this.filteredCoffeeRecords = this.coffeeRecords.filter((coffeeRecord) => {
      const recordDate = new Date(coffeeRecord.date);

      if (filterDateFrom && recordDate < filterDateFrom) {
        return false;
      }

      if (filterDateTo && recordDate > filterDateTo) {
        return false;
      }

      return true;
    });
  }

  onReset() {
    this.filterCoffeeRecordsForm = new FormGroup({
      dateFrom: new FormControl(''),
      dateTo: new FormControl(''),
    });

    this.filteredCoffeeRecords = this.coffeeRecords;
  }
}
