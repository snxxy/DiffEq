const Counter = {
    data() {
        return {
            counter: 555
        }
    }
}

Vue.createApp(Counter).mount('#counter')