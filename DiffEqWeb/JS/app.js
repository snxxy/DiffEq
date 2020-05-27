import Vue from 'vue';
import axios from 'axios';

var app = new Vue({
    el: '#app',
    data: {
        equationCount: [],
        genFormSpawn: false,
        testSpawn: false,
        sveq: 1,
        hgeq: 1,
        equations: []
    },
    methods: {
        getEquationCount: function () {
            axios.get('https://localhost:5002/api/homeapi/getequationcount').then(response => {
                this.equationCount = response.data
            })
        },
        sendGenOrder: function () {
            axios({
                method: 'POST',
                url: 'https://localhost:5002/api/homeapi',
                data: {
                    "sveq": this.sveq,
                    "hgeq": this.hgeq
                }
            })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
        sendGetEqOrder: function () {
            axios.get('https://localhost:5002/api/homeapi/getequationstosolve')
                .then(response => {
                    this.equations = response.data
                }).catch(function (error) {
                    console.log(error);
                });
        }
    },
    beforeMount() {
        this.getEquationCount()
    }
});