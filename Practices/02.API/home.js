const apiUrl = "http://localhost:3000/students"; 


async function getStudents() {
    const res = await fetch(apiUrl);
    const students = await res.json();
    document.getElementById("studentsList").innerHTML = students.map(student => `<li>${student.first_name} ${student.last_name} - ${student.dateofbirthday} (${student.sex})</li>`).join("");
}


async function addStudent(event) {
   
  
    const firstName = document.getElementById("first_name").value;
    const lastName = document.getElementById("last_name").value;
    const dateOfBirth = document.getElementById("dateofbirthday").value;
    const sex = document.getElementById("sex").value;

    if (!firstName || !lastName || !dateOfBirth || !sex) {
        document.getElementById("message").innerText = "Por favor, complete todos los campos";
        return;   
     }

    const res = await fetch(apiUrl);
    const users = await res.json();
    const newId = users.length > 0 ? (parseInt(users[users.length - 1].id) + 1).toString() : "1";
    const newStuduent = {
        id: newId,
        first_name: firstName,
        last_name: lastName,
        dateofbirthday: dateOfBirth,
        sex: sex
    };

    await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newStuduent)
    });
    document.getElementById("message").innerText = "Usuario agregado correctamente!";
    
    this.reset(); 
}

function deleteUser(userId) {
    fetch(`${apiUrl}/${userId}`, {
        method: "DELETE"
    })
    .then(() => {
        alert("Usuario eliminado");
        getStudents(); 
    })
    .catch(error => console.error("Error al eliminar el estudiante:", error));
}