<?php
	require ('inc_conn.php');
    
    // Dodati varijable koje bi spremili
	$username = mysqli_real_escape_string($conn, $_POST["?"]);
	// Mogao bi se koristiti switch za razlicita spremanja podataka
    $stmt = $conn->prepare("UPDATE;");
	$stmt->bind_param("ss", $username, $password);
	$stmt->execute() or die("7: Save query failed");
    $stmt->close();

    echo "0";
?>