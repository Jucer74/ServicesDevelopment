
# Evaluación Teórica (40%)
Responda las preguntas del siguiente Enlace:

[Examen]()

# Evaluación Práctica (60%)
Realice una Web API que permita Gestionar, las Asignaturas, los estudiantes  y los estudiantes por asignatura

- Subjects (Asignaturas)
   
| Field       | Type        | Not Null  | Description                                                       |
|-------------|-------------|-----------|-------------------------------------------------------------------|
| Id          | Int         | Not Null  | Identificador, Primary Key, Autoincremental                       | 
| Name        | Varchar(50) | Not Null  | Nombre de la Asignatura (ej: Calculo I, Ingles II, Fisica III)    |
| Room        | Varchar(10) | Not Null  | Numero del Salon, Ej 101A                                         |
| Professor   | Varchar(50) | Not Null  | Nombre completo del Profesor                                      |

- Students (Estudiantes)

| Field       | Type        | Not Null  | Description                                  |
|-------------|-------------|-----------|----------------------------------------------|
| Id          | Int         | Not Null  | Identificador, Primary Key, Autoincremental  | 
| FirstName   | Varchar(50) | Not Null  | Nombre del Estudiante                        |
| LastName    | Varchar(50) | Not Null  | Apellido del Estudiante                      |
| DateOfBirth | DateTime    | Not Null  | Fecha de Nacimiento                          |
| Sex         | Varchar(1)  | Not Null  | Sexo (solo los valores M o F son permitidos) | 

- SubjectsAndStudents (Asignaturas y Estudiantes)

| Field       | Type        | Not Null  | Description                                  |
|-------------|-------------|-----------|----------------------------------------------|
| SubjectId   | Int         | Not Null  | Llave Primaria y Referencia de la Asignatura | 
| StudentId   | Int         | Not Null  | Llave Primaria y Referencia del Estudiante   | 


## Acciones
La Web Api debe Permitir

| Action | Route                         | Description                              | Method                       | Request          | Response         | Result       |
|--------|-------------------------------|------------------------------------------|------------------------------|------------------|------------------|--------------|
| GET    | api/v1/Subjects               | Consultar todas las Asignaturas          | GetAllSubjects               |                  | List<SubjectDto> | 200-Ok       |
| GET    | api/v1/Subjects/{id}          | Consultar una Asignatura por Id          | GetSubjectById               | Id               | SubjectDto       | 200-Ok       |
| POST   | api/v1/Subjects               | Adicionar una nueva Asignatura           | CreateSubject                | SubjectDto       | SubjectDto       | 201-Created  |
| PUT    | api/v1/Subjects/{id}          | Modificar una Asignatura                 | UpdateSubject                | Id, SubjectDto   | SubjectDto       | 200-Ok       |
| DELETE | api/v1/Subjects/{id}          | Eliminar una Asignatura                  | DeleteSubject                | Id               |                  | 204-NoContent|
| GET    | api/v1/Students               | Consultar todos los Estudiantes          | GetAllStudents               |                  | List<StudentDto> | 200-Ok       |
| GET    | api/v1/Students/{id}          | Consultar un Estudiante por Id           | GetStudentById               | Id               | StudentDto       | 200-Ok       |
| POST   | api/v1/Students               | Adicionar una nueva Asignatura           | CreateStudent                | StudentDto       | StudentDto       | 201-Created  |
| PUT    | api/v1/Students/{id}          | Modificar un Estudiante                  | UPdateStudent                | Id, StudentDto   | StudentDto       | 200-Ok       |
| DELETE | api/v1/Students/{id}          | Eliminar un Estudiante                   | DeleteStudent                | Id               |                  | 204-NoContent|
| GET    | api/v1/Subjects/{id}/Students | Consultar los Estudiantes por Asignatura | GetStudentsBySubjectId       | Id               | List<StudentDto> | 200-Ok       |
| POST   | api/v1/Subjects/{id}/Student  | Adicionar un Estudiante a una Asignatura | AddStudentToSubjectById      | SubjecStudentDto |                  | 204-NoContent|
| DELETE | api/v1/Subjects/{id}/Student  | Remover un Estudiante de una Asignatura  | RemoveStudentFromSubjectById | SubjecStudentDto |                  | 204-NoContent|
| GET    | api/v1/Students/{id}/Subjects | Consultar las asignaturas por Estudiante | GetSubjectsByStudentId       | Id               | List<SubjectDto> | 200-Ok       |

## Observaciones
- Al Eliminar una Asignatura, Debe borrar todos los estudiantes inscritos a la misma sin eliminar los estudiantes (SubjectsAndStudents)
- Al Eliminar un estudiante, debe borrar todas las asignaturas que tenia inscritas este estudiante, sin eliminar la asignatura (SubjectsAndStudents)

## Condiciones
- Debe Utilizar el patron de arquitectura Limpia para desarrollar los componentes (**SchoolApp.sln**)
  - SchoolApp.WebApi.csproj
  - SchoolApp.Infrastructure.csproj
  - SchoolApp.Application.csproj
  - SchoolApp.Domain.csproj
- Debe Implementar el Modelo Basico de REST, cumpliento que:
  - La Adicion Debe mandar la Entidad sin Id y al responder debe retornar la Entidad con el nuevo Id
  - La Actualizacion mandar el Id en la Ruta y el mismo Id en la Entidad
- Se debe implementar el patron de Exception Middleware Handler y realizar las validaciones de NotFound, BadRequest e InternalServerError donde corresponda
- Debe Implementar el Patron de Generic Repository utilizando Entity Framework
- Debe Utilizar la base de datos MySQL para Almacenar los datos (**SchoolDB**)
