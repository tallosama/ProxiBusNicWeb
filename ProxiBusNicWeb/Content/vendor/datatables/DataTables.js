$(document).ready(function () {
    $('#Tabla').DataTable({
        responsive: true,
        autoWidth: false,
        dom: 'Bfrtilp',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class ="fas fa-file-excel"></i>',
                titleAttr: 'Exportar a excel',
                className: 'btn btn-primary'
            },
        ],
        language: {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "No hemos encontrado nada - disculpe :'(",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "infoEmpty": "No hay ningún registro disponible",
            "infoFiltered": "(Filtrado de _MAX_ registros)",
            "search": "Buscar:",
            "paginate": {
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
      


    }
    );
});