import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecord } from '../shared/coffee-record.interface';
import { CoffeeRecordService } from '../shared/coffee-record.service';
import { AddCoffeeRecordFormComponent } from './add-coffee-record-form/add-coffee-record-form.component';
import { DeleteCoffeeRecordModalComponent } from './delete-coffee-record-modal/delete-coffee-record-modal.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-coffee-records',
  standalone: true,
  imports: [AddCoffeeRecordFormComponent, CommonModule, DeleteCoffeeRecordModalComponent, ReactiveFormsModule],
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

  selectedCoffeeRecord: CoffeeRecord | null = null;
  showDeleteModal = false;
  showeditModal = false;

  constructor(private coffeeRecordService: CoffeeRecordService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.coffeeRecordService.CoffeeRecords.subscribe((records) => {
      this.coffeeRecords = records;
      this.filteredCoffeeRecords = records;
    });

    this.coffeeRecordService.getCoffeeRecords();
  }

  onDelete(coffeeRecord: CoffeeRecord) {
    this.selectedCoffeeRecord = coffeeRecord;
    this.showDeleteModal = true;
  }

  onConfirmDelete(id: string) {
    console.log("OnConfirmDelete", id);

    this.coffeeRecordService.deleteCoffeeRecord(id).subscribe((result) => {
      if (result) {
        this.showSuccessToastr('Coffee deleted successfully!');
      } else {
        this.showErrorToastr('Unable to delete Coffee!');
      }
    });

    this.closeDeleteModal();
  }

  closeDeleteModal() {
    this.showDeleteModal = false;
    this.selectedCoffeeRecord = null;
  }

  onFilter() {
    const filterDateFrom = this.filterCoffeeRecordsForm.value.dateFrom
      ? new Date(this.filterCoffeeRecordsForm.value.dateFrom)
      : null;

    const filterDateTo = this.filterCoffeeRecordsForm.value.dateTo
      ? new Date(this.filterCoffeeRecordsForm.value.dateTo)
      : null;

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

    this.showSuccessToastr('Coffee records filter applied!');
  }

  onReset() {
    this.filterCoffeeRecordsForm = new FormGroup({
      dateFrom: new FormControl(''),
      dateTo: new FormControl(''),
    });

    this.filteredCoffeeRecords = this.coffeeRecords;
    this.showSuccessToastr('Coffee records filter reset!');
  }
  
  showErrorToastr(message: string) {
    this.toastr.error(message, 'Error');
  }

  showSuccessToastr(message: string) {
    this.toastr.success(message, 'Success');
  }
  
  showWarningToastr(message: string) {
    this.toastr.warning(message, 'Warning');
  }
}
