import { Routes } from '@angular/router';
import { adminGuard, authGuard, staffGuard } from './core/auth.guard';
import { LoginComponent } from './features/auth/login.component';
import { ShellComponent } from './layout/shell.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { PersonalComponent } from './features/personal/personal.component';
import { InventarioComponent } from './features/inventario/inventario.component';
import { EntradasComponent } from './features/entradas/entradas.component';
import { SalidasComponent } from './features/salidas/salidas.component';
import { CuentaComponent } from './features/auth/cuenta.component';
export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: ShellComponent, canActivate: [authGuard], children: [
    { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
    { path: 'dashboard', component: DashboardComponent, canActivate: [staffGuard] },
    { path: 'inventario', component: InventarioComponent, canActivate: [staffGuard] },
    { path: 'entradas', component: EntradasComponent, canActivate: [adminGuard] },
    { path: 'salidas', component: SalidasComponent, canActivate: [staffGuard] },
    { path: 'personal', component: PersonalComponent, canActivate: [adminGuard] },
    { path: 'cuenta', component: CuentaComponent }
  ] },
  { path: '**', redirectTo: 'login' }
];
