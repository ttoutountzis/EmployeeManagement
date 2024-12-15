export const exportToCSV = (filename, rows) => {
    const csvContent = [
      Object.keys(rows[0]).join(","),
      ...rows.map((row) => Object.values(row).join(",")),
    ].join("\n");
  
    const blob = new Blob([csvContent], { type: "text/csv;charset=utf-8;" });
    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = filename;
    link.click();
  };
  