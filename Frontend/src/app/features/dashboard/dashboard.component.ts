import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: `
    <div class="bg-white rounded-xl border border-slate-200 p-6 shadow-sm">
      <h2 class="text-2xl font-bold text-[#0f5132]">Dashboard</h2>
      <p class="text-xs text-slate-500 mt-1">Panel de control y monitoreo de existencias de reactivos químicos.</p>
    </div>
  `
})
export class DashboardComponent {}