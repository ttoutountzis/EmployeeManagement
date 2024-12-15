import React, { useEffect, useState } from "react";
import axios from "../../src/app/utils/axios";
import { exportToCSV } from "../../src/app/utils/exportCSV";
import {
  Container,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  Box,
} from "@mui/material";
import { Link } from "@mui/material";

const SkillsPage = () => {
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    const fetchSkills = async () => {
      try {
        const response = await axios.get("/skills");
        setSkills(response.data);
      } catch (error) {
        console.error("Error fetching skills:", error);
      }
    };
    fetchSkills();
  }, []);

  const handleExport = () => {
    exportToCSV("skills.csv", skills);
  };

  return (
    <Container>
      <Box my={4}>
        <Typography variant="h4" gutterBottom>
          Skills
        </Typography>
        <Box display="flex" justifyContent="space-between" mb={2}>
          <Link href="/skills/new" style={{ textDecoration: "none" }}>
            <Button variant="contained" color="primary">
              Create New Skill
            </Button>
          </Link>
          <Button variant="outlined" color="secondary" onClick={handleExport}>
            Export to CSV
          </Button>
        </Box>
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {skills.map((skill) => (
                <TableRow key={skill.id}>
                  <TableCell>{skill.id}</TableCell>
                  <TableCell>{skill.name}</TableCell>
                  <TableCell>{skill.description}</TableCell>
                  <TableCell>
                    <Link href={`/skills/${skill.id}`} color="primary">
                      Edit
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

export default SkillsPage;
