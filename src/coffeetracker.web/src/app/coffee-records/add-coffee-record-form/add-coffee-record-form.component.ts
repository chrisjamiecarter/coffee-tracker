import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecordService } from '../../shared/coffee-record.service';
import { CreateCoffeeRecord } from '../../shared/create-coffee-record.interface';

@Component({
  selector: 'app-add-coffee-record-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-coffee-record-form.component.html',
  styleUrl: './add-coffee-record-form.component.css'
})
export class AddCoffeeRecordFormComponent {
  coffeeRecordService = inject(CoffeeRecordService);

  addCoffeeRecordForm = this.getAddCoffeeRecordForm();

  constructor() { }

  onSubmit() {
    const request: CreateCoffeeRecord = { name: this.addCoffeeRecordForm.value.name!, date: this.addCoffeeRecordForm.value.date!};
    console.log("Submit Form", request);
    this.coffeeRecordService.addCoffeeRecord(request);
    this.addCoffeeRecordForm = this.getAddCoffeeRecordForm();
  }

  getAddCoffeeRecordForm() {
    const today = new Date().toISOString().split('T')[0];
    return new FormGroup({
      name: new FormControl(''),
      date: new FormControl(today),
    });
  }
}
