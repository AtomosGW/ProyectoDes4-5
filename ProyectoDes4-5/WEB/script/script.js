// Variables globales
const apiUrl = 'https://localhost:5000/api/asignaciones';
const asignacionesTable = document.getElementById('asignacionesTable').getElementsByTagName('tbody')[0];
const createBtn = document.getElementById('createBtn');
const formContainer = document.getElementById('formContainer');
const asignacionForm = document.getElementById('asignacionForm');
const cancelBtn = document.getElementById('cancelBtn');
const submitBtn = document.getElementById('submitBtn');

// Funciones de notificación
function showError(message) {
    alert(message); // Puedes personalizar para mostrar el mensaje en un contenedor en el DOM
}

function showSuccess(message) {
    alert(message); // Igual que el error, se puede mostrar en un contenedor específico
}

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
        .then(response => {
            if (!response.ok) throw new Error('Error al obtener las asignaciones');
            return response.json();
        })
        .then(data => {
            asignacionesTable.innerHTML = ''; // Limpiar tabla
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
        .catch(error => {
            console.error('Error al cargar las asignaciones:', error);
            showError('No se pudieron cargar las asignaciones. Intenta nuevamente.');
        });
}

// Función para crear o editar asignación
asignacionForm.addEventListener('submit', (event) => {
    event.preventDefault();

    const assignmentId = document.getElementById('assignmentId').value;
    const ticketId = document.getElementById('ticketId').value;
    const operatorId = document.getElementById('operatorId').value;
    const assignedAt = document.getElementById('assignedAt').value;

    // Validación básica
    if (!ticketId || !operatorId || !assignedAt) {
        showError('Por favor, completa todos los campos antes de enviar.');
        return;
    }

    const asignacion = {
        assignmentId: assignmentId ? parseInt(assignmentId) : null,
        ticketId: parseInt(ticketId),
        operatorId: parseInt(operatorId),
        assignedAt: assignedAt
    };

    const method = assignmentId ? 'PUT' : 'POST';
    const url = assignmentId ? `${apiUrl}/${assignmentId}` : apiUrl;

    fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(asignacion)
    })
        .then(response => {
            if (!response.ok) throw new Error(method === 'POST' ? 'Error al crear la asignación' : 'Error al actualizar la asignación');
            loadAsignaciones();
            formContainer.style.display = 'none';
            showSuccess(method === 'POST' ? 'Asignación creada con éxito' : 'Asignación actualizada con éxito');
        })
        .catch(error => {
            console.error(error);
            showError('Hubo un error al procesar la solicitud.');
        });
});

// Función para editar asignación
function editAsignacion(id) {
    fetch(`${apiUrl}/${id}`)
        .then(response => {
            if (!response.ok) throw new Error('Error al obtener la asignación para editar');
            return response.json();
        })
        .then(asignacion => {
            document.getElementById('assignmentId').value = asignacion.assignmentId;
            document.getElementById('ticketId').value = asignacion.ticketId;
            document.getElementById('operatorId').value = asignacion.operatorId;
            document.getElementById('assignedAt').value = new Date(asignacion.assignedAt).toISOString().slice(0, -1); // Formato correcto
            formContainer.style.display = 'block';
            submitBtn.textContent = 'Actualizar Asignación';
        })
        .catch(error => {
            console.error('Error al cargar asignación para editar:', error);
            showError('Hubo un error al cargar la asignación para editar.');
        });
}

// Función para eliminar asignación
function deleteAsignacion(id) {
    if (confirm('¿Estás seguro de que quieres eliminar esta asignación?')) {
        fetch(`${apiUrl}/${id}`, {
            method: 'DELETE'
        })
            .then(response => {
                if (!response.ok) throw new Error('Error al eliminar la asignación');
                loadAsignaciones();
                showSuccess('Asignación eliminada con éxito');
            })
            .catch(error => {
                console.error('Error al eliminar asignación:', error);
                showError('Hubo un error al eliminar la asignación.');
            });
    }
}

// Cargar asignaciones al cargar la página
loadAsignaciones();