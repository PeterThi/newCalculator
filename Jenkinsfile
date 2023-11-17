pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {

        stage("Build"){
            steps{
                bat "docker compose up clearService"
            }
        }
        stage("MyDeliver"){
            steps{
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]){
                    echo $USERNAME
                    bat 'docker login -u $USERNAME -p $PASSWORD'
                    bat "docker push easvdreter/clearService"
                }
            }
        }

    }
}