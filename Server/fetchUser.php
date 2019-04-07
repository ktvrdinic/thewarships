<?php 

    require ('inc_conn.php');

    if(isset($_POST["username"])){
        $username = mysqli_real_escape_string($conn, $_POST["username"]); // Kriptiranje lozinke

        $stmt = $conn->prepare("SELECT gold, rum, wood, pearl, experience, level, no_victory, no_lose FROM users WHERE username=?;");
        $stmt->bind_param("s", $username);
        $stmt->execute() or die("12: Can't fetch data from MySQL");
        
        $rezultat = $stmt->get_result();
    
        $existinginfo = $rezultat->fetch_assoc();
        $stmt->close();
        
        echo "0_".$existinginfo["gold"]."_".$existinginfo["rum"]."_".$existinginfo["wood"]."_".$existinginfo["pearl"]."_".$existinginfo["experience"]."_".$existinginfo["level"]."_".$existinginfo["no_victory"]."_".$existinginfo["no_lose"];
    }    
?>