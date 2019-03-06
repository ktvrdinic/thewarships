<?php
	require ('inc_conn.php');
	
	$username = mysqli_real_escape_string($conn, $_POST["name"]);
	$password = mysqli_real_escape_string($conn, $_POST["password"]); // Kriptiranje lozinke

	//$upit = "SELECT username, broj_brodova, zlato, rum, drvo, biseri, score, level FROM users WHERE username=? AND lozinka=?;";


    $stmt = $conn->prepare("SELECT username, broj_brodova, zlato, rum, drvo, biseri, score, level, lozinka FROM users WHERE username=?;");
	$stmt->bind_param("s", $username);
	$stmt->execute() or die("2: Name check query failed");
	
	$rezultat = $stmt->get_result();

	//$rezultat = mysqli_query($conn, $upit) or die("2: Name check query failed");
	$existinginfo = $rezultat->fetch_assoc();
	$stmt->close();

	if($rezultat->num_rows  != 1){
		echo "5: Either no user with name or more than one";
		exit();
	}else if(!password_verify($password, $existinginfo["lozinka"])){
		echo "6: Password is not correct";
		exit();
	}
    
    
    

    echo "0\t".$existinginfo["username"]."\t".$existinginfo["broj_brodova"]."\t".$existinginfo["zlato"]."\t".$existinginfo["rum"]."\t".$existinginfo["drvo"]."\t".$existinginfo["biseri"]."\t".$existinginfo["score"]."\t".$existinginfo["level"];
?>