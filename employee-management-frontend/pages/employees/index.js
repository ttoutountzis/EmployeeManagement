import React, { useEffect, useState } from "react";
import axios from "../../src/app/utils/axios";
import {
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  TextField,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Typography,
  Box,
} from "@mui/material";
import { Link } from "@mui/material";

const EmployeesPage = () => {
  const [employees, setEmployees] = useState([]);
  const [search, setSearch] = useState("");
  const [sortBy, setSortBy] = useState("surname");
  const [selectedSkill, setSelectedSkill] = useState("");
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const response = await axios.get(`/employee`, {
          params: { search, sortBy, skillId: selectedSkill || undefined },
        });
        setEmployees(response.data);
      } catch (error) {
        console.error("Error fetching employees:", error);
      }
    };
    fetchEmployees();
  }, [search, sortBy, selectedSkill]);

  useEffect(() => {
    axios.get("/skills").then((response) => {
      setSkills(response.data);
    });
  }, []);

  return (
    <Container>
      <Box my={4}>
        <Typography variant="h4" gutterBottom>
          Employees
        </Typography>

        <Link href="/employees/new" variant="body1" color="primary">
          Create New Employee
        </Link>

        <Box display="flex" alignItems="center" gap={2} my={2}>
          <TextField
            label="Search by name"
            variant="outlined"
            size="small"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />

          <FormControl size="small" variant="outlined">
            <InputLabel>Sort By</InputLabel>
            <Select value={sortBy} onChange={(e) => setSortBy(e.target.value)} label="Sort By">
              <MenuItem value="surname">Sort by Surname</MenuItem>
              <MenuItem value="hireDate">Sort by Hire Date</MenuItem>
            </Select>
          </FormControl>

          <FormControl size="small" variant="outlined" style={{ minWidth: "100px" }}>
            <InputLabel shrink>Filter by Skill</InputLabel>
            <Select
                value={selectedSkill}
                onChange={(e) => setSelectedSkill(e.target.value)}
                label="Filter by Skill"
                displayEmpty
                renderValue={(selected) => {
                if (!selected) {
                    return <em>All</em>;
                }
                return skills.find((skill) => skill.id === selected)?.name || "";
                }}
            >
                    <MenuItem value="">
                    <em>All</em>
                    </MenuItem>
                    {skills.map((skill) => (
                    <MenuItem key={skill.id} value={skill.id}>
                        {skill.name}
                    </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </Box>

        {/* Table */}
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>First Name</TableCell>
                <TableCell>Last Name</TableCell>
                <TableCell>Hire Date</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {employees.map((emp) => (
                <TableRow key={emp.id}>
                  <TableCell>{emp.id}</TableCell>
                  <TableCell>{emp.firstName}</TableCell>
                  <TableCell>{emp.lastName}</TableCell>
                  <TableCell>{new Date(emp.hireDate).toLocaleDateString()}</TableCell>
                  <TableCell>
                    <Link href={`/employees/${emp.id}`} color="primary">
                      View
                    </Link>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
};

export default EmployeesPage;
