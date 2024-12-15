"use client";
import { Box, Button, Typography, Container, Stack } from "@mui/material";
import Link from "next/link";

export default function Home() {
  return (
    <Container
      maxWidth="sm"
      sx={{ minHeight: "100vh", display: "flex", alignItems: "center", justifyContent: "center" }}
    >
      <Box textAlign="center">
        <Typography variant="h3" component="h1" gutterBottom>
          Employee Management System
        </Typography>
        <Typography variant="subtitle1" color="text.secondary" paragraph>
          Manage your employees and their skills efficiently with ease.
        </Typography>

        <Stack direction={{ xs: "column", sm: "row" }} spacing={2} justifyContent="center">
          <Link href="/employees" passHref>
            <Button variant="contained" color="primary" size="large">
              Manage Employees
            </Button>
          </Link>
          <Link href="/skills" passHref>
            <Button variant="contained" color="success" size="large">
              Manage Skills
            </Button>
          </Link>
        </Stack>
      </Box>
    </Container>
  );
}
