// Variables globales
const apiUrl = '/api/asignaciones';  // URL de la API que manejará las asignaciones
const asignacionesTable = document.getElementById('asignacionesTable').getElementsByTagName('tbody')[0];
const createBtn = document.getElementById('createBtn');
const formContainer = document.getElementById('formContainer');
const asignacionForm = document.getElementById('asignacionForm');
const cancelBtn = document.getElementById('cancelBtn');
const submitBtn = document.getElementById('submitBtn');

// Eventos
createBtn.addEventListener('click', () => {
    formContainer.style.display = 'block';
    document.getElementById('formTitle').textContent = 'Crear Asignación';
    asignacionForm.reset();
    submitBtn.textContent = 'Crear Asignación';
});

cancelBtn.addEventListener('click', () => {
    formContainer.style.display = 'none';
});

// Función para cargar asignaciones
function loadAsignaciones() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            asignacionesTable.innerHTML = '';  // Limpiar tabla
            data.forEach(asignacion => {
                let row = asignacionesTable.insertRow();
                row.innerHTML = `
                    <td>${asignacion.assignmentId}</td>
                    <td>${asignacion.ticketId}</td>
                    <td>${asignacion.operatorId}</td>
                    <td>${new Date(asignacion.assignedAt).toLocaleString()}</td>
                    <td>
                        <button onclick="editAsignacion(${asignacion.assignmentId})">Editar</button>
                        <button onclick="deleteAsignacion(${asignacion.assignmentId})">Eliminar</button>
                    </td>
                `;
            });
        })
        .catch(error => console.error('Error al cargar las asignaciones:', error));
}

// Función para crear o editar asignación
asignacionForm.addEventListener('submit', (event) => {
    event.preventDefault();

    const assignmentId = document.getElementById('assignmentId').value;
    const ticketId = document.getElementById('ticketId').value;
    const operatorId = document.getElementById('operatorId').value;
    const assignedAt = document.getElementById('assignedAt').value;

    const asignacion = {
        assignmentId: assignmentId ? parseInt(assignmentId) : null,
        ticketId: parseInt(ticketId),
        operatorId: parseInt(operatorId),
        assignedAt: assignedAt
    };

    if (assignmentId) {
        // Actualizar asignación
        fetch(`${apiUrl}/${assignmentId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(asignacion)
        })
            .then(() => {
                loadAsignaciones();
                formContainer.style.display = 'none';
            });
    } else {
        // Crear nueva asignación
        fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(asignacion)
        })
            .then(() => {
                loadAsignaciones();
                formContainer.style.display = 'none';
            });
    }
});

// Función para editar asignación
function editAsignacion(id) {
    fetch(`${apiUrl}/${id}`)
        .then(response => response.json())
        .then(asignacion => {
            document.getElementById('assignmentId').value = asignacion.assignmentId;
            document.getElementById('ticketId').value = asignacion.ticketId;
            document.getElementById('operatorId').value = asignacion.operatorId;
            document.getElementById('assignedAt').value = new Date(asignacion.assignedAt).toISOString().slice(0, -1);  // Formato correcto
            formContainer.style.display = 'block';
            submitBtn.textContent = 'Actualizar Asignación';
        });
}

// Función para eliminar asignación
function deleteAsignacion(id) {
    if (confirm('¿Estás seguro de que quieres eliminar esta asignación?')) {
        fetch(`${apiUrl}/${id}`, {
            method: 'DELETE'
        })
            .then(() => loadAsignaciones());
    }
}

// Cargar asignaciones al cargar la página
loadAsignaciones();
