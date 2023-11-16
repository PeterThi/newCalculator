pipeline{
    agent any
    
    trigger{
        pollSCM("* * * * *")
    }
    
    stages {
        stage("Build"){
            steps{
                echo "Successfully built"
            }
        }
    }
}