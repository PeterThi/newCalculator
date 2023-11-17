pipeline{
    agent any
    
    triggers{
        pollSCM("* * * * *")
    }
    
    stages {

        stage("Build"){
            steps{
                bat "docker compose build"
            }
        }
        stage("MyDeliver"){
            steps{
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]){
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker push easvdreter/clearservice"
                    bat "docker push easvdreter/additionservice"
                    bat "docker push easvdreter/historyservice"
                    bat "docker push easvdreter/subtractionservice"
                  } 
            }
        }
        stage("Docker Swarm deployment"){
            steps{
                bat "docker stack deploy -c docker-compose.yml newcalculator"
                }
        }
    }
}