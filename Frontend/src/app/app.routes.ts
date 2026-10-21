import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login.component').then(m => m.LoginComponent)
  },
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () => import('./shared/components/layout/layout.component').then(m => m.LayoutComponent),
    children: [
      {
        path: 'dashboard',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Auxiliar', 'Almacén', 'Técnico', 'Consulta'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      // HU-003 Escenario 3: Bloqueo de inventario administrativo para Solicitante
      {
        path: 'inventario',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Auxiliar', 'Almacén', 'Técnico', 'Consulta'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'entradas',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Auxiliar', 'Almacén'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'salidas',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Auxiliar', 'Almacén', 'Técnico'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'solicitudes',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Auxiliar', 'Almacén', 'Técnico', 'Solicitante'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'reportes',
        canActivate: [roleGuard],
        data: { roles: ['Administrador', 'Encargado', 'Consulta'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      // HU-003 Escenario 1 y 2: Usuarios exclusivo para Administrador (Auxiliar bloqueado)
      {
        path: 'usuarios',
        canActivate: [roleGuard],
        data: { roles: ['Administrador'] },
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];