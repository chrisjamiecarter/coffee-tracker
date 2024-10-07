import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecordService } from '../../shared/coffee-record.service';
import { CreateCoffeeRecord } from '../../shared/create-coffee-record.interface';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-coffee-record-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-coffee-record-form.component.html',
  styleUrl: './add-coffee-record-form.component.css',
})
export class AddCoffeeRecordFormComponent {
  addCoffeeRecordForm = this.getAddCoffeeRecordForm();

  constructor(
    private coffeeRecordService: CoffeeRecordService,
    private toastr: ToastrService
  ) {}

  onSubmit() {
    const request: CreateCoffeeRecord = {
      name: this.addCoffeeRecordForm.value.name ?? '',
      date: this.addCoffeeRecordForm.value.date ?? '',
    };

    console.log('Submit Form', request);

    this.coffeeRecordService.addCoffeeRecord(request).subscribe(
      result => {
        if (result) {
          this.showSuccessToastr('Coffee recorded successfully!');
        }
        else {
          this.showErrorToastr('Unable to record Coffee!');
        }
      }
    );

    this.addCoffeeRecordForm = this.getAddCoffeeRecordForm();
  }

  getAddCoffeeRecordForm() {
    const today = new Date().toISOString().split('T')[0];
    return new FormGroup({
      name: new FormControl(''),
      date: new FormControl(today),
    });
  }

  showErrorToastr(message: string) {
    this.toastr.error(message, 'Error');
  }

  showSuccessToastr(message: string) {
    this.toastr.success(message, 'Success');
  }
}
