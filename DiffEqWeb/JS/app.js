import Vue from 'vue';
import axios from 'axios';

var app = new Vue({
    el: '#app',
    data: {
        equationCount: [],
        genFormSpawn: false,
        testSpawn: false,
        sveq: 0,
        hgeq: 0
    },
    methods: {
        isNumber: function (evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode === 46) {
                evt.preventDefault();;
            } else {
                return true;
            }
        },
        getEquationCount: function () {
            axios.get('https://localhost:5001/api/homeapi').then(response => {
                this.equationCount = response.data
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
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    },
    beforeMount() {
        this.getEquationCount()
    }
});