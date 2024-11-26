// Variables globales
const apiUrl = 'https://localhost:5001/api/asignaciones'; 
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
        .catch(error => console.error('Error al cargar las asignaciones:', error));
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
        alert('Por favor, completa todos los campos antes de enviar.');
        return;
    }

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
            .then(response => {
                if (!response.ok) throw new Error('Error al actualizar la asignación');
                loadAsignaciones();
                formContainer.style.display = 'none';
            })
            .catch(error => console.error('Error al actualizar asignación:', error));
    } else {
        // Crear nueva asignación
        fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(asignacion)
        })
            .then(response => {
                if (!response.ok) throw new Error('Error al crear la asignación');
                loadAsignaciones();
                formContainer.style.display = 'none';
            })
            .catch(error => console.error('Error al crear asignación:', error));
    }
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
        .catch(error => console.error('Error al cargar asignación para editar:', error));
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
            })
            .catch(error => console.error('Error al eliminar asignación:', error));
    }
}

// Cargar asignaciones al cargar la página
loadAsignaciones();
