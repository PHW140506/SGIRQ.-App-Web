import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Personal, Reactivo, Salida, Session, AlertasResultado, TrazabilidadItem, Reporte } from './models';
const BASE = 'http://localhost:5000/api';
@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}
  login(usuario: string, contrasenia: string) {
    return firstValueFrom(this.http.post<Session>(`${BASE}/auth/login`, { usuario, contrasenia }));
  }
  cambiarContrasenia(actual: string, nueva: string) {
    return firstValueFrom(this.http.put<void>(`${BASE}/auth/contrasenia`, { actual, nueva }));
  }
  personal(buscar = '') {
    return firstValueFrom(this.http.get<Personal[]>(`${BASE}/personal`, { params: { buscar } }));
  }
  crearPersonal(body: object) { return firstValueFrom(this.http.post(`${BASE}/personal`, body)); }
  editarPersonal(id: number, body: object) { return firstValueFrom(this.http.put(`${BASE}/personal/${id}`, body)); }
  reactivos(filtros: { buscar?: string; estadoFisico?: string; colorAnaquel?: string } = {}) {
    let params = new HttpParams();
    Object.entries(filtros).forEach(([key, value]) => { if (value) params = params.set(key, value); });
    return firstValueFrom(this.http.get<Reactivo[]>(`${BASE}/reactivos`, { params }));
  }
  crearReactivo(body: object) { return firstValueFrom(this.http.post(`${BASE}/reactivos`, body)); }
  editarReactivo(id: number, body: object) { return firstValueFrom(this.http.put(`${BASE}/reactivos/${id}`, body)); }
  entrada(idReactivo: number, cantidad: number) {
    return firstValueFrom(this.http.post<{cantidadActual:number}>(`${BASE}/entradas`, { idReactivo, cantidad }));
  }
  salida(body: object) { return firstValueFrom(this.http.post(`${BASE}/salidas`, body)); }
  salidas(desde = '', hasta = '') {
    let params = new HttpParams();
    if (desde) params = params.set('desde', desde);
    if (hasta) params = params.set('hasta', hasta);
    return firstValueFrom(this.http.get<Salida[]>(`${BASE}/salidas`, { params }));
  }
  solicitantes() {
    return firstValueFrom(this.http.get<number[]>(`${BASE}/solicitantes`));
  }
  solicitudes(idUsuario: number) {
    return firstValueFrom(this.http.get<Salida[]>(`${BASE}/solicitudes/usuario/${idUsuario}`));
  }
  alertas() {
    return firstValueFrom(this.http.get<AlertasResultado>(`${BASE}/alertas`));
  }
  trazabilidad(idReactivo: number) {
    return firstValueFrom(this.http.get<TrazabilidadItem[]>(`${BASE}/trazabilidad/reactivo/${idReactivo}`));
  }
  reportes() {
    return firstValueFrom(this.http.get<Reporte[]>(`${BASE}/reportes`));
  }
  reportePdf(tipo: string, desde = '', hasta = '') {
    let params = new HttpParams().set('tipo', tipo);
    if (desde) params = params.set('desde', desde);
    if (hasta) params = params.set('hasta', hasta);
    return firstValueFrom(this.http.get(`${BASE}/reportes/pdf`, { params, responseType: 'blob' }));
  }
}
