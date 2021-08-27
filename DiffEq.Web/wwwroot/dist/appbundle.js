
const App = {
    data() {
        return {
            equationCount: [],
            genFormSpawn: false,
            testSpawn: false,
            checked: false,
            sveq: 1,
            hgeq: 1,
            score: 0,
            equations: [],
            userSolutions: [],
            solutionColor: ["Red", "Red", "Red", "Red"]
        }
    },
    methods: {
        getEquationCount: function () {
            axios.get('https://localhost:5001/api/homeapi/getequationcount')
                .then(response => {
                    this.equationCount = response.data
                    this.refreshJax()
                })
        },
        sendGenOrder: function () {
            axios({
                method: 'POST',
                url: 'https://localhost:5001/api/homeapi',
                data: {
                    "sveq": this.sveq,
                    "hgeq": this.hgeq
                }
            })
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    console.log(error)
                });
        },
        sendGetEqOrder: function () {
            axios.get('https://localhost:5001/api/homeapi/getequationstosolve')
                .then(response => {
                    this.equations = response.data
                    setTimeout(() => this.refreshJax(), 500)
                }).catch(function (error) {
                    console.log(error)
                })
        },
        refreshJax: function () {
            MathJax.typeset()
        },
        checkInput: function () {
            this.score = 0
            for (var i = 0; i < this.equations.length; i++) {
                if (this.equations[i].solution === this.userSolutions[i]) {
                    this.score++
                    this.solutionColor[i] = "Green"
                }
                else {
                    this.solutionColor[i] = "Red"
                }
            }
        },
        toggleTestBtn: function () {
            if (this.testSpawn) {
                this.testSpawn = false
                this.genFormSpawn = false
                this.refreshTestControls()
            }
            else {
                this.testSpawn = true
                this.genFormSpawn = false
                this.sendGetEqOrder()
            }
        },
        refreshTestControls: function () {
            this.score = 0
            this.checked = false
            this.userSolutions = []
            this.equations = []
            this.solutionColor = ["Red", "Red", "Red", "Red"]
        }
    },
    beforeMount() {
        this.getEquationCount()
    }
}

Vue.createApp(App).mount('#vueEntry')