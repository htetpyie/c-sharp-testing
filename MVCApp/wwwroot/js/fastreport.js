document.addEventListener("DOMContentLoaded", function () {

    setTimeout(() => {
        // Find the toolbar's export dropdown
        const exportDropdown = document.querySelector("[title='Save File'] .fr-toolbar-dropdown-content");
        console.log(exportDropdown);
        if (exportDropdown) {
            const controller = document.querySelector("#fast-report").dataset.controller;
            // Create custom export buttons
            const createCustomButton = (label, format) => {
                const link = document.createElement("a");
                link.innerHTML = label;
                link.href = `${controller}/ExportPdf`;
                //link.href = `${controller}/ExportReport?format=${format}`;
                link.target = "_blank";
                return link;
            };
            // Add custom export buttons to the dropdown
            exportDropdown.appendChild(createCustomButton("Export to PDF", "pdf"));
            exportDropdown.appendChild(createCustomButton("Export to Excel", "excel"));
            exportDropdown.appendChild(createCustomButton("Export to Word", "word"));
        };
       
    }, 1000);
});
