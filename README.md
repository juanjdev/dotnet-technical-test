## Servicios REST Biblioteca - Net Core

La aplicación busca automatizar el comportamiento de un bibliotecario cuando un usuario desea prestar un libro. 

Un préstamo es representado en nuestro negocio por los siguientes atributos 

- isbn: Identificador único de un libro (para efectos de nuestro ejercicio, se trata de un Guid o unique identifier, ejemplo: )
- identificacionUsuario: Número de la identificación del usuario (campo alfanúmerico de máximo 10 dígitos)
- tipoUsuario: Determina la relación que tiene el usuario con la biblioteca (campo que puede tener solo un dígito númerico)
    - 1: usuario afiliado
    - 2: usuario empleado de la biblioteca
    - 3: usuario invitado

Su objetivo es crear una API tipo REST la cual permita llevar a cabo las siguientes funcionalidades 

 

1. Crear un servicio REST con el path `/api/prestamo` y el método HTTP tipo POST: Permite crear un préstamo con las siguientes validaciones 
    1. Un usuario invitado solo puede tener un libro prestado en la biblioteca, si un usuario invitado intenta prestar más de un libro debería retornar un error HTTP 400 con el siguiente JSON. 
        
        ```sql
        {
        	"mensaje": "El usuario con identificación {usuarioId} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo"
        }
        ```
        
        <aside>
        🪧 Tome especial nota del campo “mensaje” y de su valor: “identificacion” y “prestamo” no están tildados y en minúsculas y así validan las pruebas
        
        </aside>
        
    2. Para verificar si un usuario ya tiene un préstamo se debe usar el campo identificacionUsuario. El siguiente es un ejemplo de petición y un ejemplo de cómo debería ser la respuesta en un caso exitoso
        
        **Petición path: `/api/prestamo`** **Método: POST**
        
        ```json
        {
        
            "isbn":"5c493736-2762-491b-ac8d-52d06f461560",
            "identificacionUsuario":"154515485",
            "tipoUsuario":1
        }
        ```
        
        **Respuesta exitosa: HTTP Status Code 200** 
        
        ```json
        {
        		"id": "bec109ba-48bd-4393-bb4a-b3ca8c7aebad",
            "fechaMaximaDevolucion" : "15/02/2021"
        }
        ```
        
    3. Si en los campos: tipoUsuario, ISBN o identificacionUsuario llega un valor diferente a los permitidos, deberá retornar un error HTTP 400
2. Crear un servicio REST con el Path `api/prestamo/{id-prestamo}` y el método HTTP tipo GET, donde la variable *{id-prestamo}* corresponde al identificador con el cual se almacenó el préstamo en la base de datos, explicado en el primer punto. El siguiente es un ejemplo de petición y un ejemplo de cómo debería ser la respuesta en un caso exitoso
    
    **Petición path: /prestamo/bec109ba-48bd-4393-bb4a-b3ca8c7aebad**
    
    **Método: GET**
    
    ********Respuesta exitosa HTTP******** 
    
    **Status Code 200**
    
    ```json
    {
        "id": "bec109ba-48bd-4393-bb4a-b3ca8c7aebad",
        "isbn":"DASD154212",
        "identificaciónUsuario":"154515485",
        "tipoUsuario":1,
        "fechaMaximaDevolucion" : "15/02/2021"
    }
    ```
    
    ********Respuesta fallidaHTTP******** 
    
    **Status Code 404**
    
    ```json
    {
      "mensaje": "El prestamo con id bec109ba-48bd-4393-bb4a-b3ca8c7aebad no existe"
    }
    ```
    
    <aside>
    🪧 Tome especial atención de las siguientes notas:
    
    - Al momento de realizar el préstamo se debe hacer el cálculo de la fecha máxima de la devolución del libro, con la siguiente reglas de negocio:
        - Si el préstamo lo hace un usuario tipo **Afiliado**, la fecha de devolución debería ser la fecha actual más 10 días sin contar sábados y domingos
        - Si el préstamo lo hace un usuario tipo ****************Empleado****************, lla fecha de devolución debería ser la fecha actual más 8 días sin contar sábados y domingos.
        - Si el préstamo lo hace un usuario tipo ****************Invitado****************, la fecha de devolución debería ser la fecha actual más 7 días sin contar sába(dos y domingos.
        - Esta fecha deberá ser almacenada en la base de datos junto con la información del préstamo
    - La base de datos debe ser en memoria, el proveedor de base de datos en memoria ha sido inyectado en el middleware de la plantilla de este proyecto.
    - Si necesita crear modelos/entidades, cerciorarse de añadir las mismas al DbContext (PersistenceContext).
    </aside>
    
    <aside>
    📝 **Conceptos a evaluar:**
    
    - Cumplimiento de los requerimientos: para esto, hay 8 pruebas automatizadas en el proyecto PruebaIngresoBibliotecario.Api.Tests, las cuales son las encargadas de validar que usted cumpla con cada uno de los requerimientos. Estas pruebas se encuentran fallando y su objetivo es hacerlas funcionar correctamente. Usted puede ir ejecutando estas e ir validando que está cumpliendo con los requerimientos. Asegurese que al momento de enviar el código la tarea Test y Build de gradle ejecutan correctamente, esta es la manera de evaluar la prueba. Además, cuando termine la prueba, ejecute la tarea RunTest desde Hackerrank
    - Implementaciones asíncronas: de esta forma garantizamos un mejor rendimiento de la api construida (throughput)
    - Uso de Linq donde sea posible: evidencia de alguna manera el conocimiento del lenguaje de programación
    - Código limpio: Valoramos que su código sea mantenible y con principios de código limpio
    - Arquitectura: valoramos que su arquitectura propuesta demuestre una correcta separación de responsabilidades.
        - Te recomendamos hacer uso del principio de responsabilidad única
        - Te recomendamos usar un patrón de arquitectura, como hexagonal, arquitectura limpia o MVC
        - Trata de no poner la lógica de negocio en los controladores, separa tu lógica de acuerda a las restricciones del patrón de arquitectura seleccionado.
    - Pruebas unitarias (deseables): Valoramos el que logre construir pruebas unitarias a su lógica de negocio.
    
    </aside>
