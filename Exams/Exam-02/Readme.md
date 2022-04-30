
# Evaluación Teórica (40%)
Responda las preguntas del siguiente Enlace:

[Examen](https://forms.gle/eb66VSrd4L5JnCti7)


# Ejercicio (60%)
Implemente la API para la aplicacion de Reminders, desarrollando los siguientes Endpoints

## Categories
- GetAll : [GET] /api/v1/categories
- GetById : [GET] /api/v1/categories/{id}
- Add: [POST] /api/v1/categories
- Update: [PUT] /api/v1/categories/{id}
- Delete: [Delete] /api/v1/categories/{id} 

**Nota:** El Borrado de Categorias debe eliminar todos los recordatorios asociados a la misma

## Reminders
- GetAll : [GET] /api/v1/reminders
- GetById : [GET] /api/v1/reminders/{id}
- Add: [POST] /api/v1/reminders
- Update: [PUT] /api/v1/reminders/{id}
- Delete: [Delete] /api/v1/reminders/{id} 
- GetAllByCategoryId: [GET] /api/v1/reminders/category/{id}
- DeleteAllByCategoryId: [DELETE] /api/v1/reminders/category/{id}

**Nota:** El borrado de los recordatorios por id de categoria debe eliminar todos los recordatorios , sin eliminar la categoria.

# Modelo
El modelo Asociado a esta Web APi es el siguiente:

![Entidades](https://github.com/Jucer74/ServicesDevelopment/blob/main/Exams/Exam-02/img/ER-Diagram.jpg)