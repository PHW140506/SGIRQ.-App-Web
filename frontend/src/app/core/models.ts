export interface Session { token: string; idPersonal: number; nombre: string; rol: string; }
export interface Personal { idPersonal: number; nombrePersonal: string; apellidosPersonal: string;
  usuarioPersonalLogin: string; tipoPersonal: string; }
export interface Reactivo { idReactivo: number; nombreComercial: string; nombreQuimico: string;
  numeroCAS: string; estadoFisico: string; cantidadReactivo: number;
  unidadMedida: string; stockMinimo: number; colorAnaquel: string; idPersonal: number; }
export interface ItemSalida { idReactivo: number; reactivo: string; cantidadSolicitada: number; }
export interface Salida { idSalida: number; fechaSalida: string; motivoSalida: string;
  idUsuario: number; idPersonal: number; responsable: string; reactivos: ItemSalida[]; }
export const roles = ['Administrador', 'Auxiliar', 'Solicitante'];
export const estados = ['Líquido', 'Sólido'];
export const unidades = ['Mililitro', 'Gramo', 'Litro', 'Kilogramo'];
export const anaqueles = ['Verde','Rojo','Azul','Amarillo','Blanco','Naranja'];
