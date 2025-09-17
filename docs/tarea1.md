# Tarea 1: Preparación del entorno

Este documento resume las verificaciones y acciones necesarias para preparar el entorno de desarrollo y despliegue solicitado en los requerimientos del proyecto de encuestas.

## Objetivos de la tarea

1. Confirmar la disponibilidad de las herramientas principales: Visual Studio con soporte para ASP.NET (Web Forms o MVC), Entity Framework Code First, SQL Server y IIS.
2. Establecer o validar la infraestructura mínima: instancia de SQL Server y servidor con IIS listo para recibir la aplicación.
3. Documentar configuraciones y accesos necesarios para el equipo de trabajo.

## Checklist de herramientas

| Herramienta | Acción recomendada |
| --- | --- |
| Visual Studio 2019/2022 (ASP.NET, EF) | Instalar los workloads "Desarrollo de ASP.NET y web" y "Desarrollo de datos". Verificar la versión de .NET Framework requerida para el despliegue en IIS. |
| SQL Server (Developer o Express) | Confirmar que la instancia esté en ejecución y accesible desde la red de desarrollo. Crear una base de datos vacía para migraciones de EF. |
| SQL Server Management Studio (SSMS) | Validar conectividad y permisos para gestionar la base de datos. |
| IIS en servidor propio | Habilitar características de ASP.NET, configurar Application Pools con .NET CLR apropiado y habilitar despliegue remoto (Web Deploy o carpeta compartida). |

## Validaciones mínimas

1. **Conectividad a SQL Server**  
   - Probar conexión desde Visual Studio/SSMS usando la cadena de conexión planificada.  
   - Verificar permisos de creación de bases de datos, tablas y ejecución de migraciones.

2. **Servidor IIS**  
   - Comprobar que el servidor esté actualizado y con los roles/features necesarios (IIS Management Console, ASP.NET, WebSockets si aplica).  
   - Configurar un sitio de prueba para confirmar que se reciben publicaciones desde Visual Studio.

3. **Control de versiones y colaboración**  
   - Clonar el repositorio del proyecto en el equipo con Visual Studio.  
   - Configurar perfiles de publicación (Web Deploy o FTP) apuntando al servidor IIS.

## Resultados

Debido a las limitaciones del entorno de trabajo actual, no se ejecutaron instalaciones ni configuraciones reales. Sin embargo, el checklist anterior detalla los pasos concretos que el equipo debe seguir para completar la preparación del entorno antes de iniciar con el desarrollo (Tarea 2 en adelante).

## Próximos pasos

- Confirmar con el administrador de infraestructura la disponibilidad de accesos y credenciales.  
- Documentar las cadenas de conexión y URLs de publicación en un archivo seguro (por ejemplo, `appsettings.Development.json` o transformaciones de `web.config`).  
- Proceder con la creación de la solución ASP.NET una vez que los puntos anteriores estén verificados.
