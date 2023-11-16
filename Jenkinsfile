pipeline{
    agent any
    
    triggers{
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