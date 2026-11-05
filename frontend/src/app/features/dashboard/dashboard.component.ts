import { Component, OnInit } from '@angular/core';
import { DashboardViewModel } from './dashboard.viewmodel';
@Component({selector: 'app-dashboard', standalone: true, providers: [DashboardViewModel],
  templateUrl: './dashboard.component.html'})
export class DashboardComponent implements OnInit {
  constructor(public vm: DashboardViewModel) {}
  ngOnInit() { void this.vm.cargar(); }
}
